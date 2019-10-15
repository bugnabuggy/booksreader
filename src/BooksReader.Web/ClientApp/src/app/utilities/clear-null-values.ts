export function ClearNullValues(item: any): any {
    for (var field in item) {
        if (item[field] === null) {
            delete item[field];
        }
    }
    return item;
}
