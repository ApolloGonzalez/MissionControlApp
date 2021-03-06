import { Accelerator } from './accelerator';
import { Platform } from './platform';
import { MissionTeamMember } from './missionteammember';
import { Assessment } from './assessment';
import { UseCase } from './usecase';

export interface Mission {
    missionId: number;
    userId: number;
    userName: string;
    knownAs: string;
    missionName: string;
    industryAlias: string;
    businessFunctionAlias: string;
    missionStatusAlias: string;
    challenge: string;
    desiredOutcome: string;
    businessImpact: string;
    estimatedRoi: number;
    actualRoi: number;
    actualCost: number;
    timeFrame: number;
    dateCreated: Date;
    active: boolean;
    accelerators?: Accelerator[];
    platforms?: Platform[];
    missionTeam?: MissionTeamMember[];
    missionAssessment?: Assessment;
    public: boolean;
    useCases?: UseCase[];
}
