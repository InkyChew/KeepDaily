export interface IUser {
    id: number;
    name: string;
    email: string;
    password: string;
    description: string;
    emailConfirmed: boolean;
    lineId: string | null;
    lineAccessToken: string | null;
    isActive: boolean;
    userLevel: UserLevel;
    emailNotify: boolean;
    lineNotify: boolean;
    friends: IUser[];
}

export enum UserLevel {
    General = 1,
    Premium = 2
}

export interface IAuthenticateUser {
    id: number;
    level: UserLevel;
    accessToken: string;
}