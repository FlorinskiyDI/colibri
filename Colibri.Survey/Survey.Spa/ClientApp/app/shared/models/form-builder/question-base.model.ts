export class QuestionBase<T> {
    value: T;
    id: string;
    text: string;
    description: string;
    required: boolean;
    order: number;
    controlType: string;
    isAdditionalAnswer: boolean;
    options: any;
    grid: any;
    constructor(options: {
        value?: T,
        id?: string,
        text?: string,
        description?: string,
        required?: boolean,
        order?: number,
        controlType?: string,
        isAdditionalAnswer?: boolean,
        grid?: any,
        options?: any
    } = {}) {
        console.log(options);
        this.value = options.value;
        this.id = options.id || '';
        this.text = options.text || '';
        this.description = options.description || '';
        this.required = !!options.required;
        this.order = options.order === undefined ? 1 : options.order;
        this.controlType = options.controlType || '';
        this.isAdditionalAnswer = !!options.isAdditionalAnswer;
        this.grid = options.grid;
        this.options = options.options;
    }
}
