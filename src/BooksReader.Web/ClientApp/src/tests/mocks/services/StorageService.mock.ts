export class MockStorageService  implements Storage {
    [name: string]: any;    

    values : any = {};


    constructor(values?: any){
        this.values = values || {};
    }

    get length(): number {
        return Object.keys(this.values).length;
    };

    clear(): void {
        this.values = {};
    }
    getItem(key: string): string {
        return this.values[key];
    }
    key(index: number): string {
        const propName = Object.keys(this.values)[index];
        return this.values[propName];
    }
    removeItem(key: string): void {
        delete this.values[key];
    }
    setItem(key: string, value: string): void {
        this.values[key] = value;
    }


}