using PlaylistEditor.Services;
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
        public static async Task RegisterAsync(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            // サービス登録
            // youtubeサービスはAuthentication Tokenの発行処理が入り、ブラウザとのやり取りが必要となるため遅延せずに初めに必ず行う
            var youtubeService = new YouTubeServiceWrapper();
            await youtubeService.Initialize();
            services.RegisterConstant<IYouTubeService>(youtubeService);

            services.RegisterLazySingleton<IWebClientService>(() => new WebClientService());
            // VM登録
            services.RegisterLazySingleton<IPlaylistListViewViewModel>(() => {
                var vm = new PlaylistListViewViewModel(resolver.GetService<IYouTubeService>(), resolver.GetService<IWebClientService>());
				_ = vm.Initialize();
                return vm;
                });
            services.RegisterLazySingleton<ISearchResultViewViewModel>(() => new SearchResultViewViewModel(resolver.GetService<IWebClientService>()));
            services.RegisterLazySingleton<IPlaylistContentViewViewModel>(() => new PlaylistContentViewViewModel(
                resolver.GetService<IYouTubeService>(), resolver.GetService<IWebClientService>(), resolver.GetService<IPlaylistListViewViewModel>(), resolver.GetService<ISearchResultViewViewModel>()));
            services.RegisterLazySingleton<ISearchFormViewViewModel>(() => new SearchFormViewViewModel(resolver.GetService<IYouTubeService>(), resolver.GetService<ISearchResultViewViewModel>()));
            services.RegisterLazySingleton<IMainWindowViewModel>(() => new MainWindowViewModel(
				resolver.GetService<IPlaylistListViewViewModel>(),
				resolver.GetService<IPlaylistContentViewViewModel>(),
				resolver.GetService<ISearchResultViewViewModel>(),
				resolver.GetService<ISearchFormViewViewModel>()));
		}
	}
}
