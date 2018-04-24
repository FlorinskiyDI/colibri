export class QuestionBase<T> {
    value: T;
    key: string;
    label: string;
    description: string;
    required: boolean;
    order: number;
    controlType: string;
    isAdditionalAnswer: boolean;
    grid: any;
    constructor(options: {
        value?: T,
        key?: string,
        label?: string,
        description?: string,
        required?: boolean,
        order?: number,
        controlType?: string,
        isAdditionalAnswer?: boolean,
        grid?: any
    } = {}) {
        console.log(options);
        this.value = options.value;
        this.key = options.key || '';
        this.label = options.label || '';
        this.description = options.description || '';
        this.required = !!options.required;
        this.order = options.order === undefined ? 1 : options.order;
        this.controlType = options.controlType || '';
        this.isAdditionalAnswer = !!options.isAdditionalAnswer;
        this.grid = options.grid;
    }
}
