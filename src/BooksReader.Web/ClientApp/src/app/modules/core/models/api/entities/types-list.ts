import { TypeValue } from './type-value';

export interface TypesList { 
    id: number;
    name: string;
    localizationKey: string;
    
    values?: TypeValue[];
}
