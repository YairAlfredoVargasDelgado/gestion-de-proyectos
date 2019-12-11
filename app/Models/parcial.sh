x=1
veces_usado=0
veces_guardado=0
resultado_en_memoria=0

while (($x==1))
do
	echo "Calculadora básica"
	echo "Opciones:"
	echo "1. Suma"
	echo "2. Restar "
	echo "3. Multiplicador"
	echo "4. Dividir"
	echo "5. Guardar resultado en memoria"
	echo "6. Mostrar resultados en memoria"
	echo "7. Salir"
	echo "Digite la opción deseada: "
	read opcion

	if ((opcion != 5 && opcion != 6 && opcion != 7))
	then
		echo "Ingrese el primer número"
		read primer_numero
		echo "Ingrese el segundo número"
		read segundo_numero
	fi

	if (($opcion == 1))
	then
		if (($veces_guardado==1))
		then
			resultado=`expr $primer_numero + $segundo_numero + $resultado_en_memoria`
		else
			resultado=`expr $primer_numero + $segundo_numero`
		fi
		echo "El resultado es: "
		echo $resultado
	fi



	if (($opcion == 2))
	then
		if (($veces_guardado==1))
		then
			resultado=`expr $resultado_en_memoria - $primer_numero - $segundo_numero`
		else
			resultado=`expr $primer_numero  $segundo_numero`
		fi
		echo "El resultado es: "
		echo $resultado

	fi

	if (($opcion == 3))
	then
		if (($veces_guardado==1))
		then
			resultado=$((primer_numero*segundo_numero*resultado_en_memoria))
		else
			resultado=$((primer_numero*segundo_numero))
		fi
		echo "El resultado es: "
		echo $resultado
	fi

	if (($opcion == 4))
	then
		if (($veces_guardado==1))
		then
			resultado=$((resultado_en_memoria/primer_numero/segundo_numero))
		else
			resultado=$((primer_numero/segundo_numero))
		fi
		echo "El resultado es: "
		echo $resultado
	fi

	if (($opcion == 5))
	then
		if (($veces_usado==0))
		then
			echo "Debe realizar una operación antes"
		else
			resultado_en_memoria=$resultado;
			echo "Resultado guardado en memoria"
			veces_guardado=1
		fi
	fi

	if (($opcion == 6))
	then
		if (($veces_guardado==0))
		then
			echo "No se ha guardado nada"
		else
			echo "El resultado en memoria es: "
			echo $resultado_en_memoria
		fi
	fi

	if (($opcion == 7))
	then
		x=0
	fi

	veces_usado=1
done