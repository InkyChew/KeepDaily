export interface IMessage {
    id: number;
    type: MessageType;
    userId: number;
    title: string;
    content: string;
    link: string;
    image: string | null;
    createdTime: Date;
    isRead: boolean;
}

export enum MessageType {
    Friend = 1
}