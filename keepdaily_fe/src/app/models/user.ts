import { Plan } from "./calendar";

export interface IUser {
    id: number;
    name: string;
    email: string;
    password: string;
    description: string;
    imgName?: string;
    imgType?: string;
    emailConfirmed: boolean;
    lineId?: string;
    lineAccessToken?: string;
    isActive: boolean;
    userLevel: UserLevel;
    emailNotify: boolean;
    lineNotify: boolean;
    plans: Plan[];
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

export class Friend {
    userId: number;
    friendId: number;

    constructor(uid: number, fid: number) {
        this.userId = uid;
        this.friendId = fid;
    }
}