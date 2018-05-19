export class Helpers {

    static endsWithSlash(url: String) {
        if (url) {
            return url.endsWith('/') ? url : `${url}/`;
        }

        console.error('!!! The value is null');
        return null;
    }

    static deleteCookie(name: string) {
        document.cookie = name + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    }


    static availabilityObjNames<T>(constructorFn: new () => T, fields: string[]) {
        const obj = new constructorFn();
        const propertyNames = Object.keys(obj);
        fields.forEach(fieldName => {
            const result = propertyNames.indexOf(fieldName);
            if (result < 0) {
                console.error(`The "${fieldName}" field is missing from the object`);
            }
        });

        return fields;
    }

}
