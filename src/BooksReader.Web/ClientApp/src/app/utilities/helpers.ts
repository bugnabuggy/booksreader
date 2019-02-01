export function flatten(obj) {
    let result = Object.create({});
    // tslint:disable-next-line:forin
    for (let key in obj) {
        result[key] = obj[key];
    }
    return result;
}
