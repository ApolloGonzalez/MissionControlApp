import { User } from './user';

export interface Assessment {
    id: number;
    userId: number;
    missionId: number;
    dataLocation: string;
    dataType: string;
    dataDomainExpert: string;
    challengeSolution: string;
    dataQuality: string;
    challengeType: string;
    infrastructureRequirement: string;
    accuracyRequirement: string;
    active: boolean;
    dateCreated: Date;
    user: User;
}
