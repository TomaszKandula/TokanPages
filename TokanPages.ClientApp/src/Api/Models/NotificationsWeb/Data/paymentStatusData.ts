export interface PaymentStatusData {
    paymentStatus: string;
    isActive: boolean;
    autoRenewal: boolean;
    totalAmount: string;
    currencyIso: string;
    extCustomerId: string;
    extOrderId: string;
    expiresAt: string;
}
