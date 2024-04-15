import { ContainNumber } from "./containNumber";
import { HasProperty } from "./hasProperty";
import { HaveLargeLetter } from "./haveLargeLetter";
import { HaveSmallLetter } from "./haveSmallLetter";
import { HaveSpecialCharacter } from "./haveSpecialCharacter";
import { 
    PASSWORD_MISSING_CHAR, 
    PASSWORD_MISSING_NUMBER, 
    PASSWORD_MISSING_LARGE_LETTER, 
    PASSWORD_MISSING_SMALL_LETTER 
} from "../../../constants";

export const containNumber = (password: string, result: any): void => {
    if (!ContainNumber(password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + PASSWORD_MISSING_NUMBER]
            : [PASSWORD_MISSING_NUMBER];
        result = { ...result, password: data };
    }
}

export const haveSpecialCharacter = (password: string, result: any): void => {
    if (!HaveSpecialCharacter(password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + PASSWORD_MISSING_CHAR]
            : [PASSWORD_MISSING_CHAR];
        result = { ...result, password: data };
    }
}

export const haveLargeLetter = (password: string, result: any): void => {
    if (!HaveLargeLetter(password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + PASSWORD_MISSING_LARGE_LETTER]
            : [PASSWORD_MISSING_LARGE_LETTER];
        result = { ...result, password: data };
    }
}

export const haveSmallLetter = (password: string, result: any): void => {
    if (!HaveSmallLetter(password)) {
        const data = HasProperty(result, "password")
            ? [...result.password, "Password " + PASSWORD_MISSING_SMALL_LETTER]
            : [PASSWORD_MISSING_SMALL_LETTER];
        result = { ...result, password: data };
    }
}
