import { Injectable } from '@angular/core';



@Injectable({
  providedIn: 'root'
})
/**
 * Implements Storage interface and uses local storage for web version
 */
export class StorageService implements Storage {
  storage: Storage;
  [name: string]: any;

  length: number;

  constructor() {
    // by default use local storage
    this.storage =  localStorage;
  }

  clear(): void {
    this.storage.clear();
  }

  getItem(key: string): string {
    return this.storage.getItem(key) || null;
  }

  key(index: number): string {
    return this.storage.key(index);
  }

  removeItem(key: string): void {
    return this.storage.removeItem(key);
  }

  setItem(key: string, value: string): void {
    return this.storage.setItem(key, value);
  }

}
