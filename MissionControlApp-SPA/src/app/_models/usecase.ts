export interface UseCase {
    name: string[];
    description: string[];
    commonuses: string[];
    visuals: Visual[];
    benefits: string[];
}

export interface Visual {
    imglocation: string;
    description: string;
}
