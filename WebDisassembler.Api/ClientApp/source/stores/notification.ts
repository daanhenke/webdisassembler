import {defineStore} from "pinia";

export interface Notification
{
    title: string;
    body: string;
}

interface NotificationStoreState
{
    pendingNotifications: Notification[]
}

export const useNotificationStore = defineStore('notification', {
    state: () => {
        return <NotificationStoreState> {
            pendingNotifications: []
        }
    },
    actions: {
        addNotification(notification: Notification) {
            this.pendingNotifications.push(notification);
        },
        popNext(): Notification {
            return this.pendingNotifications.shift();
        }
    }
})