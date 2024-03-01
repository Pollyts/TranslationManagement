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