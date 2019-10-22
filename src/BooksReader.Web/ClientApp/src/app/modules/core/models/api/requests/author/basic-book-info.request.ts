import { IIdentifiable } from '../../entities';

export interface BasicBookInfo extends IIdentifiable {
    title: string;
    author: string;
}
