export interface MissionTeamMember {
    id: number;
    missionId: number;
    userId: number;
    userName: string;
    knownAs: string;
    employee: boolean;
    jobTitle: string;
    age: number;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    city: string;
    country: string;
}
