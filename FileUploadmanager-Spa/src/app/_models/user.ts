export interface User {
  id: number;
  userName: string;
  knownAs: string;
  age: number;
  gender: string;
  created: string;
  city: string;
  country: string;
  roles?: string[];
}
