export class CookiesStorage implements Storage {
    [name: string]: any;    
    length: number;

    clear(): void {
        throw new Error("Method not implemented.");
    }
    getItem(key: string): string {
        throw new Error("Method not implemented.");
    }
    key(index: number): string {
        throw new Error("Method not implemented.");
    }
    removeItem(key: string): void {
        throw new Error("Method not implemented.");
    }
    setItem(key: string, value: string): void {
        throw new Error("Method not implemented.");
    }
}