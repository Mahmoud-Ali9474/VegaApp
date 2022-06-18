import { Contact } from './Contact';
import { KeyValuePairModel } from './KeyValuePairModel';
export interface VehicleModel {
  id: number;
  model: KeyValuePairModel;
  make: KeyValuePairModel;
  isRegistered: boolean;
  features: KeyValuePairModel[];
  lastUpdate: Date;
  contact: Contact;
}
