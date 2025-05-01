import { FacebookModelDto } from "./facebookModelDto";
import { TwitterModelDto } from "./twitterModelDto";

export interface MetaModelDto {
    language: string;
    facebook: FacebookModelDto;
    twitter: TwitterModelDto;
}
