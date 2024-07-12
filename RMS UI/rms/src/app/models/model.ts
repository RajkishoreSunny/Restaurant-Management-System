export interface loginModel
{
    email: string,
    password: string
}

export interface menuCategory
{
    categoryId: number,
    categoryName: string,
    description: string,
    categoryImg: string
}

export interface menu
{
    menuId: number,
    categoryId: number,
    name: string,
    description: string,
    price: number,
    itemImg: string,
    rating: number
}

export interface orders
{
    orderId?: number,
    customerId: number,
    orderDate: Date,
    totalPrice: number,
    menuId: number,
    quantity: number,
    itemImg?: string,
    name?: string
}

export interface customer
{
    customerId?: number,
    customerName: string,
    customerEmail: string,
    customerPhone: string,
    customerAddress: string,
    password: string
}

export interface getCustomerDetails
{
    customerName: string,
    customerEmail: string,
    customerPhone: string,
    customerAddress: string
}

export interface payment
{
    orderId: number,
    amount: number,
    paymentDateTime: Date
}

export interface table
{
    tableId: number,
    seatingCapacity: number,
    status: string
}

export interface reservation
{
    customerId: number,
    reservationDateTime: Date,
    numberOfPeople: number,
    status: boolean,
    tableId?: number
}

export interface about
{
    name: string,
    address: string,
    phoneNo?: string,
    openingTime?: Date
}