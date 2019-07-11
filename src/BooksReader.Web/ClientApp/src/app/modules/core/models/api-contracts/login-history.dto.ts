export interface LoginHistory {
    id?:string;
    userId?:string;
    
    dateTime: string;
    ipAddress: string;
    browser: string;
    geolocation: string;
}
