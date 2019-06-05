import { Accelerator } from './accelerator';

export interface Mission {
    missionId: number;
    userId: number;
    username: string;
    missionName: string;
    industryAlias: string;
    businessFunctionAlias: string;
    challenge: string;
    desiredOutcome: string;
    timeFrame: number;
    dateCreated: Date;
    active: boolean;
    // accelerators?: Accelerator[];
}
