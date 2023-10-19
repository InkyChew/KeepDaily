import { IUser } from "./user";

export class Plan {
    id: number = 0;
    title: string = "My Big plan";
    description: string = "Descript your big plan here!";
    startFrom: string;
    updateTime: Date = new Date();    
    days: Day[] = [];
    userId: number;
    user?: IUser;
    categoryId: number | null = null;
    category?: Category;

    constructor(uid: number, today: string) {
        this.userId = uid;
        this.startFrom = today;
    }
}

export class Calendar {
    year: number;
    month: number;
    days: Day[] = [];

    constructor(year: number, month: number, days: Day[]) {
        this.year = year;
        this.month = month;
        this.days = days;
    }
}

export interface Day {
    id: number;
    year: number;
    month: number;
    date: number;
    imgName: string | null;
    imgType: string | null;
    planId: number;
}

export class Category {
    id: number = 0;
    name: string = "";
    name_zh: string = "";
}