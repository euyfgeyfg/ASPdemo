export type User = {
  id: string;
  name: string;
  email: string;
  token: string;
  imageUrl?: string;
};

export type LoginCreds = {
  email: string;
  password: string;
};

export type RegisterCreds = {
  email: string;
  name: string;
  password: string;
};
