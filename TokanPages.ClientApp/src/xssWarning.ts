import { GetDataFromStorage } from "./Shared/Services/StorageServices";
import { SELECTED_LANGUAGE } from "./Shared/constants";
import Validate from "validate.js";

const selfXssWarnignConfig = {
    banner: {
        color: "#ec0000",
        fontWeight: "bold",
        fontSize: "35px",
    },
    secondLine: {
        fontWeight: "bold",
        fontSize: "15px",
    },
};

const selfXssWarningContent = {
    pol: {
        bannerText: "UWAGA!",
        firstLineText: "Ta funkcja przeglądarki jest przeznaczona wyłącznie dla programistów serwisu.",
        secondLineText:
            "Jeśli ktoś polecił Ci edytować stronę w ten sposób, jest to próba ataku i wyłudzenia danych.\nNic tutaj nie wklejaj ani nie wpisuj.",
        thirdLineText: "Zamknij okno tego narzędzia (kliknij X w prawym, górnym rogu).\nPoinformuj nas o próbie ataku.",
    },
    eng: {
        bannerText: "WARNING!",
        firstLineText: "This browser feature is intended only for the website developers.",
        secondLineText:
            "If someone told you edit a page in this way, it is an attempt of attack and phishing.\nDo not paste or type anything here.",
        thirdLineText:
            "Close the tool window (click the X in the top right corner).\nInform us about an attempted attack.",
    },
};

export const printSelfXssWarning = (): void => {
    const id = GetDataFromStorage({ key: SELECTED_LANGUAGE });
    const languageId = !Validate.isString(id) ? "eng" : (id as "eng" | "pol");
    const translations = selfXssWarningContent[languageId];

    setTimeout(() => {
        console.log(
            "%c" + translations.bannerText,
            "color:" +
                selfXssWarnignConfig.banner.color +
                "; font-weight:" +
                selfXssWarnignConfig.banner.fontWeight +
                ";" +
                "font-size:" +
                selfXssWarnignConfig.banner.fontSize +
                ";"
        );
        console.log(translations.firstLineText);
        console.log(
            "%c" + translations.secondLineText,
            "font-weight:" +
                selfXssWarnignConfig.secondLine.fontWeight +
                ";" +
                "font-size:" +
                selfXssWarnignConfig.secondLine.fontSize +
                ";"
        );
        console.log(translations.thirdLineText);
    }, 2000);
};
