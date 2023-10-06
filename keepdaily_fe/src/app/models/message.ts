export interface IMessage {
    id: number;
    userId: number;
    title: string;
    content: string;
    image: string | null;
    createdTime: string;
    isRead: boolean;
}