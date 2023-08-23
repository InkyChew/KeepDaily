export class Plan {
    id: number = 0;
    title: string = "My Big plan";
    description: string = "Descript your big plan here!";
    startFrom: string;
    updateTime: Date = new Date();    
    days: Day[] = [];
    // calendars: Calendar[] = [];

    constructor(today: string) {
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
    imgName?: string;
    imgType?: string;
    planId: number;
}