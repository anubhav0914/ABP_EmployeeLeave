import { AppConsts } from '@shared/AppConsts';
import { UtilsService } from 'abp-ng2-module';

export class SignalRAspNetCoreHelper {
    static initSignalR(callback?: () => void): void {
        const encryptedAuthToken = new UtilsService().getCookieValue(AppConsts.authorization.encryptedAuthTokenName);

        abp.signalr = {
            autoConnect: true,
            connect: undefined,
            hubs: undefined,
            qs: AppConsts.authorization.encryptedAuthTokenName + '=' + encodeURIComponent(encryptedAuthToken),
            remoteServiceBaseUrl: AppConsts.remoteServiceBaseUrl,
            startConnection: undefined,
            url: '/signalr',
            withUrlOptions: {}
        };

        const script = document.createElement('script');
        if (callback) {
            script.onload = () => {
                console.log("✅ abp.signalr-client.js loaded");

            // 🔁 Register notification handler
            abp.event.on('abp.notifications.received', (userNotification) => {
                console.log('📩 Notification received: ', userNotification);
                // You can show a toast or custom notification here
            });

            // 🚀 Connect manually
            abp.signalr.connect();
                callback();
            };
        }
        script.src = AppConsts.appBaseUrl + '/assets/abp/abp.signalr-client.js';
        document.head.appendChild(script);
    }
}
