import { Injectable } from '@angular/core';
import { isPlatformBrowser, isPlatformServer } from '@angular/common';
import { CookiesStorage } from '@br/utilities/cookies-storage';

@Injectable({
  providedIn: 'root'
})
export class StorageService implements Storage {
  private storage: Storage;
  
  [name: string]: any;

  length: number;

  constructor() {
    // by default use local storage
    this.storage =  isPlatformBrowser 
    ? localStorage
    : new CookiesStorage();
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
