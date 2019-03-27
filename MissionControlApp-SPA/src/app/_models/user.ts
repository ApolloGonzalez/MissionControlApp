import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    knownAs: string;
    email: string;
    created: Date;
    lactActive: Date;
    photoUrl: string;
    city: string;
    country?: string;
    photos?: Photo[];
}
