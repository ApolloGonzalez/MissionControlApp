import { Photo } from './photo';

export interface User {
  id: number;
  userName: string;
  knownAs: string;
  employee: boolean;
  jobTitle: string;
  age: number;
  gender: string;
  created: Date;
  lastActive: any;
  photoUrl: string;
  city: string;
  country: string;
  experience?: string;
  introduction?: string;
  interests?: string;
  photos?: Photo[];
  roles?: string[];
}
