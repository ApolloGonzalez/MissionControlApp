export interface MissionTeamMember {
    id: number;
    missionId: number;
    userId: number;
    userName: string;
    knownAs: string;
    age: number;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    city: string;
    country: string;
}
