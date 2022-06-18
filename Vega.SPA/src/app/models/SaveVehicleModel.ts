import { Contact } from './Contact';
export interface SaveVehicleModel {
  id: number;
  modelId: number | undefined;
  makeId: number | undefined;
  isRegistered: boolean;
  features: number[];
  contact: Contact;
}
