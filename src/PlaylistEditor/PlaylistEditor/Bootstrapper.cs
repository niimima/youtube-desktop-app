﻿using PlaylistEditor.Services;
using PlaylistEditor.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor
{
    public static class Bootstrapper
    {
        /// <summary>
        /// DIで取得するインスタンスを登録します
        /// </summary>
        /// <param name="services"></param>
        /// <param name="resolver"></param>
        /// <remarks>
        /// 以下を参照して作成。
        /// https://dev.to/ingvarx/avaloniaui-dependency-injection-4aka
        /// </remarks>
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            // サービス登録
            services.RegisterLazySingleton<IYouTubeService>(() => {
                var service = new YouTubeServiceWrapper();
                service.Initialize();
                return service;
            });
            services.RegisterLazySingleton<IWebClientService>(() => new WebClientService());

            // VM登録
            services.RegisterLazySingleton<IPlaylistListViewViewModel>(() => new PlaylistListViewViewModel());
            services.RegisterLazySingleton<IPlaylistContentViewViewModel>(() => new PlaylistContentViewViewModel());
            services.RegisterLazySingleton<ISearchResultViewViewModel>(() => new SearchResultViewViewModel(resolver.GetService<IWebClientService>()));
            services.RegisterLazySingleton<ISearchFormViewViewModel>(() => new SearchFormViewViewModel(resolver.GetService<IYouTubeService>(), resolver.GetService<ISearchResultViewViewModel>()));
            services.RegisterLazySingleton<IMainWindowViewModel>(() => new MainWindowViewModel(
				resolver.GetService<IPlaylistListViewViewModel>(),
				resolver.GetService<IPlaylistContentViewViewModel>(),
				resolver.GetService<ISearchResultViewViewModel>(),
				resolver.GetService<ISearchFormViewViewModel>()));
		}
	}
}
