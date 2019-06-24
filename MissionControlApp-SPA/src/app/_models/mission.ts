import { Accelerator } from './accelerator';
import { Platform } from './platform';

export interface Mission {
    missionId: number;
    userId: number;
    userName: string;
    knownAs: string;
    missionName: string;
    industryAlias: string;
    businessFunctionAlias: string;
    challenge: string;
    desiredOutcome: string;
    timeFrame: number;
    dateCreated: Date;
    active: boolean;
    accelerators?: Accelerator[];
    platforms?: Platform[];
    public: boolean;
}
