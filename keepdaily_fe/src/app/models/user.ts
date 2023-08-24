export interface IUser {
    id: number;
    name: string;
    email: string;
    password: string;
    emailConfirmed: boolean;
    lineId: string | null;
    lineAccessToken: string | null;
    isActive: boolean;
    level: UserLevel;
    notify: NotifyType;
    friends: IUser[];
}

export enum UserLevel {
    General = 1,
    Premium = 2
}

export enum NotifyType {
    None = 0,
    Email = 1,
    Line = 2,
    All = 3
}