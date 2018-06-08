// import isEqual from 'lodash/isEqual';
// import { isEqual, reduce } from 'lodash';
// import _ from 'lodash';
export class CompareObject {

    constructor() {

    }



    // private static checkeFirstObject(result: any, value: any, key: any, a: any, b: any) {

    //     debugger
    //     if (b.hasOwnProperty(key)) {
    //         if (isEqual(value, b[key])) {
    //             return result;
    //         } else {
    //             if (typeof (a[key]) !== typeof ({}) || typeof (b[key]) !== typeof ({})) {
    //                 // dead end.
    //                 result.different.push(key);
    //                 return result;
    //             } else {
    //                 const deeper = this.compareTwoObject(a[key], b[key]);
    //                 result.different = result.different.concat(_.map(deeper.different, (sub_path) => {
    //                     return key + '.' + sub_path;
    //                 }));

    //                 result.missing_from_second = result.missing_from_second.concat(_.map(deeper.missing_from_second, (sub_path) => {
    //                     return key + '.' + sub_path;
    //                 }));

    //                 result.missing_from_first = result.missing_from_first.concat(_.map(deeper.missing_from_first, (sub_path) => {
    //                     return key + '.' + sub_path;
    //                 }));
    //                 return result;
    //             }
    //         }
    //     } else {
    //         result.missing_from_second.push(key);
    //         return result;
    //     }
    // }

    // private static checkeSecondObject(result: any, value: any, key: any, a: any) {
    //     if (a.hasOwnProperty(key)) {
    //         return result;
    //     } else {
    //         result.missing_from_first.push(key);
    //         return result;
    //     }
    // }


    public static compareTwoObject(a: any, b: any) {
        const result: any = {
            different: [],
            missing_from_first: [],
            missing_from_second: []
        };


        // reduce(a, this.checkeFirstObject(result, value: any, this.key, a, b), result);
        // reduce(b, this.checkeSecondObject(result, this.value, this.key, a), result);


        // reduce(a, function (result, value, key) {
        //     if (b.hasOwnProperty(key)) {
        //         if (isEqual(value, b[key])) {
        //             return result;
        //         } else {
        //             if (typeof (a[key]) !== typeof ({}) || typeof (b[key]) !== typeof ({})) {
        //                 // dead end.
        //                 result.different.push(key);
        //                 return result;
        //             } else {
        //                 const deeper = this.compareTwoObject(a[key], b[key]);
        //                 result.different = result.different.concat(_.map(deeper.different, (sub_path) => {
        //                     return key + '.' + sub_path;
        //                 }));

        //                 result.missing_from_second = result.missing_from_second.concat(_.map(deeper.missing_from_second, (sub_path) => {
        //                     return key + '.' + sub_path;
        //                 }));

        //                 result.missing_from_first = result.missing_from_first.concat(_.map(deeper.missing_from_first, (sub_path) => {
        //                     return key + '.' + sub_path;
        //                 }));
        //                 return result;
        //             }
        //         }
        //     } else {
        //         result.missing_from_second.push(key);
        //         return result;
        //     }
        // }, result);

        // reduce(b, function (result, value, key) {
        //     if (a.hasOwnProperty(key)) {
        //         return result;
        //     } else {
        //         result.missing_from_first.push(key);
        //         return result;
        //     }
        // }, result);

        return result;
    }
}
