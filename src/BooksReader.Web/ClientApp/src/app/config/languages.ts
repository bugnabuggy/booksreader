import { Language } from '@br/core/models';

export class Languages {
    static  get (): Language[] {
        return [
            {
                code:'ru',
                name:'Russian',
                flag:''
            },
            {
                code:'en',
                name:'English',
                flag:''
            }
        ]
    }
}