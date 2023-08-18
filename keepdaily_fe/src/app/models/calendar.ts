
export class Plan {
    id: number = 0;
    title: string = "My Big plan";
    description: string = "Descript your big plan here!";
    startFrom: Date = new Date();
    calendars: Calendar[] = [];
}

export class Calendar {
    year: number;
    month: number;
    days: Day[] = [];

    constructor(year: number, month: number) {
        this.year = year;
        this.month = month;
    }
}

export class Day {
    date: number;
    imgUrl?: string

    constructor(date: number) {
        this.date = date;
    }
}