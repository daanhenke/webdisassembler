import {defineStore} from "pinia";

export enum NotificationStatus
{
    Working,
    Done
}

interface Notification
{
    status: NotificationStatus;
    title: string;
    body: string;
}

interface NotificationStoreState
{
    map: Map<number, Notification>,
    nextId: number
}

export const useNotificationStore = defineStore('notification', {
    state: () => {
        return <NotificationStoreState> {
            map: new Map<number, Notification>(),
            nextId: 0
        }
    },
    getters: {
        hasNotifications(): boolean {
            return Array.from(this.map.keys()).length > 0;
        },
        notifications(): Notification[] {
            const keys = Array.from(this.map.keys()).sort();
            return keys.map(key => this.map.get(key));
        }
    },
    actions: {
        createNotification(title: string, body: string, finishInstantly: boolean): number {
            const id = this.nextId++;
            
            this.map[id] = {
                title,
                body,
                status: finishInstantly ? NotificationStatus.Done : NotificationStatus.Working
            };
            
            return id;
        },
        updateNotification(id: number, body: string) {
            if (this.map.has(id)) {
                this.map[id].body = body;
            }
        },
        finishNotification(id: number) {
            if (this.map.has(id)) {
                this.map[id].status = NotificationStatus.Done;
                setTimeout(this.removeNotification.bind(this, id), 2000);
            }
        },
        removeNotification(id: number) {
            this.map.delete(id);
        }
    }
})