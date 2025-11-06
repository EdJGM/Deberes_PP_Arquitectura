import { NextRequest, NextResponse } from 'next/server';

const BACKEND_URL = 'http://localhost:8080/ConUni_RestFull_Java_GR06/api/ConversorUnidades';

// Maneja GET requests
export async function GET(
  request: NextRequest,
  context: { params: Promise<{ path: string[] }> }
) {
  const { path: pathArray } = await context.params;
  const path = pathArray.join('/');
  const searchParams = request.nextUrl.searchParams.toString();
  const url = `${BACKEND_URL}/${path}${searchParams ? `?${searchParams}` : ''}`;

  try {
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    const data = await response.json();
    return NextResponse.json(data, { status: response.status });
  } catch (error) {
    console.error('Error en proxy GET:', error);
    return NextResponse.json(
      { error: 'Error de conexión con el servidor' },
      { status: 500 }
    );
  }
}

// Maneja POST requests
export async function POST(
  request: NextRequest,
  context: { params: Promise<{ path: string[] }> }
) {
  const { path: pathArray } = await context.params;
  const path = pathArray.join('/');
  const url = `${BACKEND_URL}/${path}`;

  try {
    const body = await request.json();

    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body),
    });

    const data = await response.json();
    return NextResponse.json(data, { status: response.status });
  } catch (error) {
    console.error('Error en proxy POST:', error);
    return NextResponse.json(
      { error: 'Error de conexión con el servidor' },
      { status: 500 }
    );
  }
}

// Maneja PUT requests
export async function PUT(
  request: NextRequest,
  context: { params: Promise<{ path: string[] }> }
) {
  const { path: pathArray } = await context.params;
  const path = pathArray.join('/');
  const url = `${BACKEND_URL}/${path}`;

  try {
    const body = await request.json();

    const response = await fetch(url, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body),
    });

    const data = await response.json();
    return NextResponse.json(data, { status: response.status });
  } catch (error) {
    console.error('Error en proxy PUT:', error);
    return NextResponse.json(
      { error: 'Error de conexión con el servidor' },
      { status: 500 }
    );
  }
}

// Maneja DELETE requests
export async function DELETE(
  request: NextRequest,
  context: { params: Promise<{ path: string[] }> }
) {
  const { path: pathArray } = await context.params;
  const path = pathArray.join('/');
  const url = `${BACKEND_URL}/${path}`;

  try {
    const response = await fetch(url, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    const data = await response.json();
    return NextResponse.json(data, { status: response.status });
  } catch (error) {
    console.error('Error en proxy DELETE:', error);
    return NextResponse.json(
      { error: 'Error de conexión con el servidor' },
      { status: 500 }
    );
  }
}