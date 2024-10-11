from multiprocessing import Pool, cpu_count
import time

# Con multiprocessing calculo pi hasta el término 1 millón, utilice al menos 3 procesadores.

def calcular_terminos_nilakantha(rango):
    inicio, fin = rango
    suma_parcial = 0
    for i in range(inicio, fin):
        k = i * 2
        if i == 0:
            continue
        denominador = k * (k + 1) * (k + 2)
        if i % 2 == 1:
            suma_parcial += 4 / denominador
        else:
            suma_parcial -= 4 / denominador
    
    return suma_parcial

def dividir_trabajo(total_terminos, num_procesos):
    tamano_bloque = total_terminos // num_procesos
    rangos = []
    for i in range(num_procesos):
        inicio = i * tamano_bloque
        fin = inicio + tamano_bloque if i < num_procesos - 1 else total_terminos
        rangos.append((inicio, fin))
    return rangos

def calcular_pi_paralelo(total_terminos=1000000, num_procesos=3):
    num_procesos = min(num_procesos, cpu_count())
    rangos = dividir_trabajo(total_terminos, num_procesos)
    
    with Pool(num_procesos) as pool:
        resultados = pool.map(calcular_terminos_nilakantha, rangos)
    pi_aprox = 3 + sum(resultados)
    return pi_aprox

if __name__ == '__main__':
    TOTAL_TERMINOS = 1000000
    NUM_PROCESOS = 4
    
    print(f"Calculando pi usando la formula de Nilakantha\n")
    print(f"Con {NUM_PROCESOS} procesos y {TOTAL_TERMINOS} terminos")
    
    tiempo_inicio = time.time()
    resultado = calcular_pi_paralelo(TOTAL_TERMINOS, NUM_PROCESOS)
    tiempo_fin = time.time()
    
    print(f"PI = {resultado:.30f}")
    print(f"Tiempo de calculo: {tiempo_fin - tiempo_inicio:.2f} s")