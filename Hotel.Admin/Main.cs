using Autofac;
using Autofac.Integration.Mvc;
using CacheManager.Core;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin
{
    public class Main
    {
        public static void Start()
        {
            var builder = new ContainerBuilder();
            initPubDI(builder);
            // registerController(builder);
            RegisterContainerHelper(builder);
        }

        private static void initPubDI(ContainerBuilder builder, bool defaultUserAuth = true)
        {
            builder.Register(c => new Log4NetLogAdapter(AppDomain.CurrentDomain.BaseDirectory + "Config/log4net.config")).As<ILogAdapter>().SingleInstance();

            //注册缓存CacheFactory
            int redisCacheServerPort;
            var redisCacheServer = ConfigurationManager.AppSettings["RedisCacheServer"];
            var redisCacheServerPwd = ConfigurationManager.AppSettings["RedisCacheServerPwd"];
            int.TryParse(ConfigurationManager.AppSettings["RedisCacheServerPort"], out redisCacheServerPort);
            var cache = CacheFactory.Build("RuntimeCache", settings =>
            {
                settings.WithSystemRuntimeCacheHandle("RuntimeCache")
                .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromMinutes(10))
                .And
                .WithRedisConfiguration("RedisUserCache", config =>
                {
                    config.WithAllowAdmin()
                    .WithDatabase(15)
                    .WithPassword(redisCacheServerPwd)
                    .WithEndpoint(redisCacheServer, redisCacheServerPort);
                })
                .WithMaxRetries(100)
                .WithRetryTimeout(50)
                .WithRedisBackPlate("RedisUserCache")
                .WithRedisCacheHandle("RedisUserCache", true)
                .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromMinutes(30));
            });

            var presistentCache = CacheFactory.Build("RuntimeCache2", settings =>
            {
                settings.WithSystemRuntimeCacheHandle("RuntimeCache2")
                .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromMinutes(60))
                .And
                .WithRedisConfiguration("RedisPresistentCache", config =>
                {
                    config.WithAllowAdmin()
                    .WithDatabase(15)
                    .WithPassword(redisCacheServerPwd)
                    .WithEndpoint(redisCacheServer, redisCacheServerPort);
                })
                .WithMaxRetries(100)
                .WithRetryTimeout(50)
                .WithRedisBackPlate("RedisPresistentCache")
                .WithRedisCacheHandle("RedisPresistentCache", true)
                .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromDays(30));
            });

            var tokenCache = CacheFactory.Build("RuntimeTokenCache", settings =>
            {
                settings.WithSystemRuntimeCacheHandle("RuntimeTokenCache")
                .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromMinutes(60))
                .And
                .WithRedisConfiguration("RedisTokenCache", config =>
                {
                    config.WithAllowAdmin()
                    .WithDatabase(15)
                    .WithPassword(redisCacheServerPwd)
                    .WithEndpoint(redisCacheServer, redisCacheServerPort);
                })
                .WithMaxRetries(100)
                .WithRetryTimeout(50)
                .WithRedisBackPlate("RedisTokenCache")
                .WithRedisCacheHandle("RedisTokenCache", true)
                .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromDays(30));
            });

            DefaultCacheFactory.InitCacheInstance(cache, presistentCache, tokenCache, 1);

            //注册ID生成器
            byte serverID = 0;
            byte.TryParse(ConfigurationManager.AppSettings["IDGenServerID"], out serverID);
            builder.Register(c => new DefaultIdBuilder(serverID, "RedisQueueConfig")).As<IdBuilder>().SingleInstance();  //NextIntID与Queue保存在同台Redis中持久化


            //是否使用默认用户验证等信息
            if (defaultUserAuth)
            {
                //服务
                //用户业务逻辑
                builder.Register(c => new UserBL()).As<IUserBL>().SingleInstance();
                //containerBuilder.Register(c => new AdminUserBL()).As<AdminUserBL>().SingleInstance();

                //用户身份认证
                builder.Register(c => new AuthenticationBL()).As<IAuthenticationBL>().InstancePerRequest();
                //containerBuilder.Register(c => new FormsAuthenticationBL()).As<IAuthenticationBL>().InstancePerApiRequest();

                //用户权限处理
                builder.Register(c => new PermissionBL()).As<IPermissionBL>().SingleInstance();

                //用户操作日志处理
                builder.Register(c => new OprtLogBL()).As<IOprtLogBL>().SingleInstance();
            }
        }

        /// <summary>
        /// 注册容器组件
        /// </summary>
        /// <param name="containerBuilder"></param>
        public static void RegisterContainerHelper(ContainerBuilder containerBuilder)
        {
            //注册组件--------------------------------
            var container = containerBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); //注册到mvc DI容器中以获取到每请求缓存的实例

            ContainerHelper.RegisterContainer(container);
        }
    }
}