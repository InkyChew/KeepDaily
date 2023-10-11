export interface IMessage {
    id: number;
    userId: number;
    title: string;
    content: string;
    link: string;
    image: string | null;
    createdTime: Date;
    isRead: boolean;
}