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

}
