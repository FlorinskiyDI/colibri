export class AvailableQuestions {
    constructor(
        public type: string,
        public name: string,
        public order: number,
        public dropZonesName: string,
        public description: string,
        public icon: string // awesome icon name
    ) { }
}
