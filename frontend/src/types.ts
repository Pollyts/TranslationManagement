export interface Translator {
	id: number;
	name: string;
	hourlyRate: string;
	status: number;
	creditCardNumber: string;
	jobs: { id: number; name: string }[];
  }

export interface TranslationJob {
	id: number;
	customerName: string;
	status: string;
	price: number;
	translator: { id: number; name: string };
  }

  export enum TranslatorStatus {
    Applicant = 0,
    Certified = 1,
    Deleted = 2,
  }

  export const TranslatorStatusOptions = [
    { id: TranslatorStatus.Applicant, name: 'Applicant' },
    { id: TranslatorStatus.Certified, name: 'Certified' },
    { id: TranslatorStatus.Deleted, name: 'Deleted' },
  ];

  
