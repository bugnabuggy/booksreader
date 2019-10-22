import { Observable } from 'rxjs';

export class Base64FileLoader {
    static loadFile(file): Observable<string> {
        const observable = Observable.create( (observer) => {
            if (!file) {
                observer.error('File cant be empty!');
                observer.complete();
                return;
            }

            const reader = new FileReader();
            reader.onload = () => {
                observer.next(reader.result as string);
                observer.complete();
            };
            reader.readAsDataURL(file);
        });

        return observable;
    }
}
