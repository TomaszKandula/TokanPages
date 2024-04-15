import { ContainNumber } from "./containNumber";
import { HasProperty } from "./hasProperty";
import { HaveLargeLetter } from "./haveLargeLetter";
import { HaveSmallLetter } from "./haveSmallLetter";
import { HaveSpecialCharacter } from "./haveSpecialCharacter";
import { PASSWORD_MISSING_CHAR, PASSWORD_MISSING_NUMBER, PASSWORD_MISSING_LARGE_LETTER, PASSWORD_MISSING_SMALL_LETTER } from "Shared/constants";

export const haveSpecialCharacter = (newPassword: string, result: any): void => {
    if (!HaveSpecialCharacter(newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + PASSWORD_MISSING_CHAR]
            : [PASSWORD_MISSING_CHAR];
        result = { ...result, newPassword: data };
    }
}

export const containNumber = (newPassword: string, result: any): void => {
    if (!ContainNumber(newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + PASSWORD_MISSING_NUMBER]
            : [PASSWORD_MISSING_NUMBER];
        result = { ...result, newPassword: data };
    }
}

export const haveLargeLetter = (newPassword: string, result: any): void => {
    if (!HaveLargeLetter(newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + PASSWORD_MISSING_LARGE_LETTER]
            : [PASSWORD_MISSING_LARGE_LETTER];
        result = { ...result, newPassword: data };
    }
}

export const haveSmallLetter = (newPassword: string, result: any): void => {
    if (!HaveSmallLetter(newPassword)) {
        const data = HasProperty(result, "newPassword")
            ? [...result.newPassword, "New password " + PASSWORD_MISSING_SMALL_LETTER]
            : [PASSWORD_MISSING_SMALL_LETTER];
        result = { ...result, newPassword: data };
    }
}
