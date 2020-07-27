export class WorkflowHistory {
    steps: Step[];
}

export class Step {
    statusId: number;
    description: string;
    results: Result[];
}

export class Result {
    user?: string;
    userName?: string;
    date?: string;
}
